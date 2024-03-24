import { Component, OnInit, inject } from '@angular/core';
import { MenuComponent } from "./components/menu/menu.component";
import { PageHeaderComponent } from "./components/page-header/page-header.component";
import { BookItemComponent } from "./components/book-item/book-item.component";
import { CommonModule } from '@angular/common';
import { LoginComponent } from './pages/login/login.component';
import { RouterOutlet, Router } from '@angular/router';
import { AuthService } from './services/auth-service';

@Component({
    selector: 'app-root',
    standalone: true,
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css'],
    imports: [MenuComponent, PageHeaderComponent, BookItemComponent, CommonModule, RouterOutlet, LoginComponent]
})
export class AppComponent implements OnInit {
  authService = inject(AuthService);
  title = 'reader';
  constructor(private router: Router){}
  ngOnInit(): void {
      if (true){//!this.authService.isLoginedIn()){
        //this.router.navigateByUrl('/login');
        return;
      }

      this.router.navigateByUrl('/');
  }
}
