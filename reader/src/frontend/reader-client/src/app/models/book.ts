import { AbstractItem } from "./abstract-list-item"
export interface BookItem{
    bookId?: string
    bookName?: string
    authorFirstName?: string
    authorLastName?: string
}

export interface Book{
    Id: string,
    Name: string,
    Author: string,
    Category: string, 
}

export interface BookWithDescription extends BookItem{
    description?: string
}

export interface BookToRead extends BookItem{
    text?: string
}

export class BookListItem implements AbstractItem{
    elementId: number = 1;
    name: string ='';
    id: string ='';
    readonly type: string = 'book';
    info: string = '';
    toInfo(): void {
        this.info = 'author:' +this.author;
    }
    author : string =''

}