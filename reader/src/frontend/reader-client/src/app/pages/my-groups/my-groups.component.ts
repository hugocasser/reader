import { Component, OnInit, inject } from '@angular/core';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { MenuComponent } from '../../components/menu/menu.component';
import { GroupService } from '../../services/groups-service';
import { AuthService } from '../../services/auth-service';
import { GroupItemComponent } from "../../components/group-item/group-item.component";
import { CommonModule } from '@angular/common';

@Component({
    selector: 'app-my-groups',
    standalone: true,
    templateUrl: './my-groups.component.html',
    styleUrl: './my-groups.component.css',
    imports: [PageHeaderComponent, MenuComponent, GroupItemComponent, CommonModule]
})
export class MyGroupsComponent implements OnInit {
setId(id: string) {
  localStorage.setItem('MyGroupId', id);
}
  curentPage: number = 1;
  pageSize = localStorage.getItem("pageSize");
  groups: any[] = [];
  title = 'My-groups';
  groupsService = inject(GroupService);
  auth = inject(AuthService);

  ngOnInit(): void {
    this.next();
  }

  next() {
    this.curentPage++;
    this.getGroups();
  }

  prev() {
    this.curentPage--;
    this.getGroups();
  }

  getGroups() {
    this.groups = [];
    try {
      let groups = this.groupsService.getGroupByUserId(Number(this.pageSize), this.curentPage);
      groups.forEach(this.groups.push);
    }
    catch {
      let result = this.auth.refreshToken();
      if (result === true) {
        let groups = this.groupsService.getGroupByUserId(Number(this.pageSize), this.curentPage);
        groups.forEach(this.groups.push);
      }
      else {
        this.auth.logout();
      }
    }
  }
}
