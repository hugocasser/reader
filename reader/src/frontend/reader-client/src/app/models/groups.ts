import { BookItem } from "./book"
import { UserItem } from "./user"
import { AbstractItem } from "./abstract-list-item"

export interface GroupItem {
    admin: any;
    groupId: string;
    groupName: string;
    books: BookItem[];
    users: UserItem[];
    notes: any[];
}


export class GroupListItem implements AbstractItem{
    elementId: number = 1;
    toInfo(): void {
        this.info = 'admin: ' +this.admin + '\n users count: ' +this.users + '\n books count: ' +this.books;
    }

    name: string ='';
    id: string ='';
    readonly type ='group';
    info: string ='';
    users: number = 0;
    admin: string ='';
    books: number = 0;

}
