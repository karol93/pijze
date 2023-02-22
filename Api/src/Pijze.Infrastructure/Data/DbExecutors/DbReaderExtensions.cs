using System.Collections;
using System.Data.Common;
using System.Runtime.Serialization;

namespace Pijze.Infrastructure.Data.DbExecutors;

internal static class DbReaderExtensions
{
    internal static async Task<T?> FirstOrDefault<T>(this DbDataReader reader)
    {
        var r = await reader.ReadAsync();
        if (!r) return default;
        var dtoItem = CreateDtoItem<T>(reader);
        return dtoItem;

    }
    
    internal static async Task<IEnumerable<T>> Query<T>(this DbDataReader reader)
    {
        var dtoList = new List<T>();
        while (await reader.ReadAsync())
        {
            var dtoItem = CreateDtoItem<T>(reader);
            dtoList.Add(dtoItem);
        }

        return dtoList;
    }

    internal static async Task<IEnumerable<T>> Query<T>(this DbDataReader reader,
        IDictionary<string, string> collectionMappingConfig)
    {
        if (!collectionMappingConfig.Any())
            throw new Exception("Collection mapping config cannot be empty");

        var collectionRegistry = BuildCollectionRegistry<T>(collectionMappingConfig);

        var dtoList = new List<T>();
        var dtoItem = (T) FormatterServices.GetUninitializedObject(typeof(T));
        while (await reader.ReadAsync())
        {
            var dtoKeyPropertyValue = typeof(T).GetProperty(reader.GetName(0)).GetValue(dtoItem);
            var dbKeyColumnValue = reader[0];

            if (dtoKeyPropertyValue != null && dbKeyColumnValue.ToString() != dtoKeyPropertyValue.ToString())
            {
                foreach (var key in collectionRegistry.Keys)
                {
                    InitCollection(key, dtoItem, collectionRegistry);
                    collectionRegistry[key] = Activator.CreateInstance(collectionRegistry[key].GetType()) as IList;
                }

                dtoList.Add(dtoItem);
                dtoItem = (T) FormatterServices.GetUninitializedObject(typeof(T));
            }

            var mainPropertiesColumnMaxIndex =
                reader.GetOrdinal(collectionMappingConfig.First().Value) - 1;
            for (var i = 0; i <= mainPropertiesColumnMaxIndex; i++)
            {
                PopulateDto(reader, i, dtoItem);
            }

            if (reader.FieldCount == mainPropertiesColumnMaxIndex)
                return dtoList;

            var collectionsColumnsStartIndex = mainPropertiesColumnMaxIndex + 1;

            var currentCollectionPropertyName = collectionMappingConfig
                .First(x => x.Value == reader.GetName(collectionsColumnsStartIndex)).Key;

            var collectionItemObject = InitCollectionItemObject(collectionRegistry, currentCollectionPropertyName);

            PopulateDto(reader, collectionsColumnsStartIndex, collectionItemObject);
            for (var i = collectionsColumnsStartIndex + 1; i < reader.FieldCount; i++)
            {
                var dbColumnName = reader.GetName(i);
                var newCollectionPropertyInfo = collectionMappingConfig
                    .FirstOrDefault(x => x.Value == dbColumnName).Key;
                PopulateDto(reader, i, collectionItemObject);
                if (newCollectionPropertyInfo is null && i + 1 == reader.FieldCount)
                {
                    collectionRegistry[currentCollectionPropertyName].Add(collectionItemObject);
                }
                else if (newCollectionPropertyInfo != null &&
                         currentCollectionPropertyName != newCollectionPropertyInfo)
                {
                    collectionRegistry[currentCollectionPropertyName].Add(collectionItemObject);
                    currentCollectionPropertyName = newCollectionPropertyInfo;
                }
            }
        }

        foreach (var key in collectionRegistry.Keys)
        {
            InitCollection(key, dtoItem, collectionRegistry);
        }

        dtoList.Add(dtoItem);
        return dtoList;
    }

    private static IDictionary<string, IList> BuildCollectionRegistry<T>(
        IDictionary<string, string> collectionMappingConfig)
    {
        var collectionRegistry = new Dictionary<string, IList>();
        foreach (var (propertyName, _) in collectionMappingConfig)
        {
            var property = typeof(T).GetProperty(propertyName);
            if (property is null || property.PropertyType == typeof(string) ||
                !typeof(IEnumerable).IsAssignableFrom(property.PropertyType))
            {
                throw new Exception($"Invalid map option for property {propertyName}");
            }

            var listType = typeof(List<>);
            var constructedListType = listType.MakeGenericType(property.PropertyType.GetGenericArguments()[0]);
            var list = Activator.CreateInstance(constructedListType) as IList;
            collectionRegistry.Add(propertyName, list);
        }

        return collectionRegistry;
    }

    private static object GetValue(this DbDataReader reader, int i, Type type)
    {
        if (type == typeof(byte[]))
            return (byte[]) reader[i];
        
        return Type.GetTypeCode(type) switch
        {
            TypeCode.Int16 => reader.GetInt16(i),
            TypeCode.Int32 => reader.GetInt32(i),
            TypeCode.Boolean => reader.GetBoolean(i),
            TypeCode.Char => reader.GetChar(i),
            TypeCode.Int64 => reader.GetInt64(i),
            TypeCode.Double => reader.GetDouble(i),
            TypeCode.Decimal => reader.GetDecimal(i),
            TypeCode.DateTime => reader.GetDateTime(i),
            TypeCode.String => reader.GetString(i),
            _ => reader.GetValue(i)
        };
    }

    private static object InitCollectionItemObject(IDictionary<string, IList> dict, string collectionPropertyName) =>
        FormatterServices.GetUninitializedObject(dict[collectionPropertyName].GetType().GetGenericArguments()[0]);

    private static void PopulateDto(DbDataReader reader, int i, object dto)
    {
        var dbColumnName = reader.GetName(i);
        var dtoPropertyInfo = dto.GetType().GetProperty(dbColumnName);
        var dbValue = reader.IsDBNull(i) ? null : reader.GetValue(i, dtoPropertyInfo.PropertyType);
        dtoPropertyInfo?.SetValue(dto, dbValue);
    }

    private static void InitCollection<T>(string collectionPropertyName, T dtoItem, IDictionary<string, IList> dict)
    {
        var dtoItemPropertyInfo = typeof(T).GetProperty(collectionPropertyName);
        if (dtoItemPropertyInfo is null)
            throw new Exception($"Property '{collectionPropertyName}' not found");
        dtoItemPropertyInfo.SetValue(dtoItem, dict[collectionPropertyName]);
    }
    
    
    private static T CreateDtoItem<T>(DbDataReader reader)
    {
        var dtoItem = (T) FormatterServices.GetUninitializedObject(typeof(T));
        for (var i = 0; i < reader.FieldCount; i++)
        {
            PopulateDto(reader, i, dtoItem!);
        }

        return dtoItem;
    }
}