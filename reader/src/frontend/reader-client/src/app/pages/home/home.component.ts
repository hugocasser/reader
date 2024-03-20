import { Component } from '@angular/core';
import { MenuComponent } from '../../components/menu/menu.component';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [MenuComponent, PageHeaderComponent],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent {
title: string = 'About';
}
