import { Component, OnInit, OnDestroy, inject } from '@angular/core';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { MenuComponent } from '../../components/menu/menu.component';
import { LoginComponent } from '../login/login.component';
import { CommonModule } from '@angular/common';
import { Input } from '@angular/core';
import { BooksListComponent } from '../../components/books-list/books-list.component';
import { UsersListComponent } from '../../components/users-list/users-list.component';
import { GroupService } from '../../services/groups-service';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth-service';

@Component({
  selector: 'app-group-page',
  standalone: true,
  imports: [PageHeaderComponent, MenuComponent, LoginComponent, CommonModule, BooksListComponent, UsersListComponent],
  templateUrl: './group-page.component.html',
  styleUrl: './group-page.component.css'
})
export class GroupPageComponent implements OnInit, OnDestroy {
  @Input() name: string = '';
  @Input() groupId: string | null = localStorage.getItem('groupId');
  groups = inject(GroupService);
  auth = inject(AuthService);
  router = inject(Router);
  users: any[] = [];
  books: any[] = [];

  title = "";
  loginedIn: boolean = false;

  ngOnInit(): void {
    if (sessionStorage.getItem('groupId') === null){
      this.router.navigateByUrl('/groups')
      return;
    }
    else{
      let id = '';
      if (this.groupId !== null){
        id = this.groupId;
      }
      try{
        let group = this.groups.getGroupById(id);
        group.forEach((g => {
          this.users = g.users;
          this.books = g.books;
          this.name = g.name;
        }));
      }
      catch{
        let result = this.auth.refreshToken()
        if (result === true){
          let group = this.groups.getGroupById(id);
          group.forEach((g => {
            this.users = g.users;
            this.books = g.books;
            this.name = g.name;
          }));
        }
        else{
          this.auth.logout();
        }
      }
    }
    this.title= 'Group: ' + this.name;
  }

  ngOnDestroy(): void {
    sessionStorage.removeItem('groupId')
  }
}
