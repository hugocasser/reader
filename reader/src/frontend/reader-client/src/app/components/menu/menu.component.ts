import { Component, OnInit, inject } from '@angular/core';
import { MatIconModule } from '@angular/material/icon';
import { CommonModule } from '@angular/common';
import { MenuItemComponent } from '../menu-item/menu-item.component';
import { Router} from '@angular/router';


@Component({
    standalone: true,
    selector: 'app-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.css'],
    imports: [CommonModule, MatIconModule, MenuItemComponent]
})
export class MenuComponent implements OnInit {
  menuItems: any[] = [
    {name: 'About reader', link: '/about', path: 'assets/home-icon.png'},
    {name: 'All Books', link: '/books', path: 'assets/books-icon.png'},
    {name: 'Groups', link: '/groups', path: 'assets/groups-icon.png'},
    {name: 'My Groups', link: '/my-groups', path: 'assets/myGroups-icon.png'},
    {name: 'Reading', link: '/reading-page', path: 'assets/reading-icon.png'},
    {name: 'Settings', link: '/settings', path: 'assets/settings-icon.png'}
  ];

  isOpen: boolean =false;
  isClicked: boolean =true;
  router = inject(Router);
  ngOnInit(): void {
    this.isOpen = false;
    this.isClicked = true;
    
    if (localStorage.getItem('IsAdmin') === null){
        this.menuItems.push({name: 'Admin panel', link: '/admin', path: 'assets/admin-icon.png'});
    }

  }
  
  toggleMenu() {
    this.isOpen = !this.isOpen;
    this.isClicked = !this.isClicked
  } 

  redirect(link: string){
    this.router.navigateByUrl(link);
  }
}

