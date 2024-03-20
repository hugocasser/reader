import { Component } from '@angular/core';
import { UsersListItemComponent } from "../users-list-item/users-list-item.component";

@Component({
    selector: 'app-users-list',
    standalone: true,
    templateUrl: './users-list.component.html',
    styleUrl: './users-list.component.css',
    imports: [UsersListItemComponent]
})
export class UsersListComponent {
    isVisible = false;
    UsersList() {
        this.isVisible = !this.isVisible
    }

}
