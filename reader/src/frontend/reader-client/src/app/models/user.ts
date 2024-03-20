import { AbstractItem } from "./abstract-list-item";

export interface User {
    email: string;
    firstName?: string;
    lastName?: string;
    userName?: string;
    password?: string;
  }
  
export interface UserLogin {
    email: string;
    password: string;
  }

export interface UserItem{
  userId?: string
  firstName?: string
  lastName?: string
}

export class UserListItem implements AbstractItem{
  elementId: number = 1;
  name: string = '';
  id: string = '';
  readonly type: string ='user';
  info: string = '';
  roles: string[] = [];
  toInfo(): void {
    this.roles.forEach(role => {
      this.info += role;
    })
  }

}
  