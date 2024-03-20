import { Component, Input } from '@angular/core';
import { UsersListItemComponent } from "../users-list-item/users-list-item.component";
import {NgFor} from "@angular/common";

@Component({
    selector: 'app-users-list',
    standalone: true,
    templateUrl: './users-list.component.html',
    styleUrl: './users-list.component.css',
    imports: [UsersListItemComponent, NgFor]
})
export class UsersListComponent {
    isVisible = false;
    @Input() users: any[] = [];
    UsersList() {
        this.isVisible = !this.isVisible
    }

}
