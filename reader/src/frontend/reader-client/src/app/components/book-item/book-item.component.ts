import { Component, Input, OnInit } from '@angular/core';
import { BookInfoComponent } from "../book-info/book-info.component";
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-book-item',
    standalone: true,
    templateUrl: './book-item.component.html',
    styleUrl: './book-item.component.css',
    imports: [BookInfoComponent, CommonModule]
})
export class BookItemComponent implements OnInit{
  @Input() authorFirtstName: string = '';
  @Input() authorLastName: string = '';
  @Input() bookName: string = '';
  @Input() description = '';
  @Input() bookId = '';
  showInfo: boolean = false;
  isHover = false;

  ngOnInit(): void {
    
  }

  openInfo(){
    this.showInfo = !this.showInfo;
  }

  return(){
    if (this.showInfo){
      this.showInfo = !this.showInfo;
    }
  }

  infoLink(){
    if (this.isHover){
      return 'assets/info-black.png';
    }
    else{
      return 'assets/info.png';
    }
  }
}
