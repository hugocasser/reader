import { Component } from '@angular/core';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { MenuComponent } from '../../components/menu/menu.component';

@Component({
  selector: 'app-my-groups',
  standalone: true,
  imports: [PageHeaderComponent, MenuComponent],
  templateUrl: './my-groups.component.html',
  styleUrl: './my-groups.component.css'
})
export class MyGroupsComponent {
  title = 'My Groups'
}
