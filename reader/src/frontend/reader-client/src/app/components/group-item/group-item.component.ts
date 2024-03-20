import { Component, Input, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-group-item',
  standalone: true,
  imports: [],
  templateUrl: './group-item.component.html',
  styleUrl: './group-item.component.css'
})
export class GroupItemComponent implements OnInit{

  constructor(private router: Router){}

  @Input() name: string = '';
  @Input() admin: string = '';
  @Input() books: number = 0;
  @Input() users: number = 0;
  @Input() groupId = '';

  isHover = false;

  ngOnInit(): void {
    
  }

  openInfo(){
    sessionStorage.setItem('groupId', this.groupId);
    this.router.navigateByUrl('/group/:id');
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
