import { Component, Input, OnInit } from '@angular/core';
import { LoginComponent } from "../../pages/login/login.component";
import { Router } from '@angular/router';

@Component({
    selector: 'app-page-header',
    standalone: true,
    templateUrl: './page-header.component.html',
    styleUrl: './page-header.component.css',
    imports: [LoginComponent]
})
export class PageHeaderComponent implements OnInit{
  @Input() title: string  = ''
  loginedAs: string = '';
  isSignedIn = false;
  showLogoutPanel = false;

  constructor(private router: Router){}

  ngOnInit(): void {
    let loginedAsUser = localStorage.getItem('UserName');

    if (loginedAsUser != null){
      this.loginedAs = ('signed as: ' + loginedAsUser);
      return;
    }
    this.loginedAs = 'signed as: test'
    this.isSignedIn = true;
  }

  logout(){
    this.router.navigateByUrl('logout');
  }
}
