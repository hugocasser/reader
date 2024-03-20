import { Component, Input, } from '@angular/core';
import { inject } from '@angular/core';
import { Router, RouterOutlet, RouterLink, RouterLinkActive} from '@angular/router';

@Component({
  selector: 'app-menu-item',
  standalone: true,
  imports: [RouterOutlet, RouterLink, RouterLinkActive],
  templateUrl: './menu-item.component.html',
  styleUrl: './menu-item.component.css'
})

export class MenuItemComponent {
  router = inject(Router);
  @Input() link: string = '';
  @Input() name: string = '';

}
