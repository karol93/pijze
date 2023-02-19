import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from './core';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss'],
})
export class AppComponent implements OnInit {
  isLogged: boolean = false;
  constructor(private authService: AuthService, private router: Router) {}
  ngOnInit(): void {
    // this.isLogged = this.authService.isAuthenticated();
  }

  title = 'Pijze.Client';
}
