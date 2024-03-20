import { Component, OnInit, OnDestroy } from '@angular/core';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { MenuComponent } from '../../components/menu/menu.component';
import { LoginComponent } from '../login/login.component';
import { CommonModule } from '@angular/common';
import { Input } from '@angular/core';
import { BooksListComponent } from '../../components/books-list/books-list.component';
import { UsersListComponent } from '../../components/users-list/users-list.component';

@Component({
  selector: 'app-group-page',
  standalone: true,
  imports: [PageHeaderComponent, MenuComponent, LoginComponent, CommonModule, BooksListComponent, UsersListComponent],
  templateUrl: './group-page.component.html',
  styleUrl: './group-page.component.css'
})
export class GroupPageComponent implements OnInit, OnDestroy {
  @Input() name: string | null = '';
  @Input() groupId: string | null = '';

  title = "";
  loginedIn: boolean = false;

  ngOnInit(): void {
    this.name = sessionStorage.getItem('groupId');
    this.title= 'Group: ' + this.name;
  }

  ngOnDestroy(): void {
    sessionStorage.removeItem('groupId')
  }
}
