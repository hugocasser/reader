import { Component, OnInit, inject} from '@angular/core';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { MenuComponent } from '../../components/menu/menu.component';
import { CommonModule } from '@angular/common';
import { GroupItemComponent } from '../../components/group-item/group-item.component';
import { GroupService } from '../../services/groups-service';
import { AuthService } from '../../services/auth-service';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-groups',
  standalone: true,
  imports: [PageHeaderComponent, MenuComponent, CommonModule, GroupItemComponent],
  templateUrl: './groups.component.html',
  styleUrl: './groups.component.css'
})
export class GroupsComponent implements OnInit {
  curentPage: number = 1;
  pageSize = localStorage.getItem("pageSize");
  groups: any[] = [];
  title = 'Groups';
  groupsService = inject(GroupService);
  auth = inject(AuthService);

  ngOnInit(): void {
    this.next();
  }

  next(){
    this.curentPage++;
    this.getGroups();
  }

  prev(){
    this.curentPage--;
    this.getGroups();
  }

  getGroups(){
    this.groups = [];
    try{
      let groups = this.groupsService.getAllGroups(this.curentPage, Number(this.pageSize));
      groups.forEach(this.groups.push);
    }
    catch{
      let result = this.auth.refreshToken();
      if (result === true){
        let groups = this.groupsService.getAllGroups(this.curentPage, Number(this.pageSize));
        groups.forEach(this.groups.push);
      }
      else{
        this.auth.logout();
      }
    }
    finally{
      
    }
  }
}
