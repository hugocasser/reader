import { Routes } from '@angular/router';
import { BooksComponent } from './pages/books/books.component';
import { HomeComponent } from './pages/home/home.component';
import { SettingsComponent } from './pages/settings/settings.component';
import { GroupPageComponent } from './pages/group-page/group-page.component';
import { BookInfoComponent } from './components/book-info/book-info.component';
import { GroupsComponent } from './pages/groups/groups.component';
import { ReadingPageComponent } from './pages/reading-page/reading-page.component';
import { RegisterComponent } from './pages/register/register.component';
import { LoginComponent } from './pages/login/login.component';
import { MyGroupsComponent } from './pages/my-groups/my-groups.component';
import { LogoutComponent } from './components/logout/logout.component';
import { AdminPanelComponent } from './pages/admin-panel/admin-panel.component';

export const routes: Routes = [
    {
        path: 'books',
        component : BooksComponent
    },
    {
        path:'about',
        component: HomeComponent
    },
    {
        path: '',
        redirectTo: '/about',
        pathMatch: 'full'
    },
    {
        path: 'settings',
        component: SettingsComponent
    },
    {
        path:'groups',
        component: GroupsComponent
    },
    {
        path: 'book-info/',
        component: BookInfoComponent
    },
    {
        path: 'reading-page',
        component: ReadingPageComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: 'group/:id',
        component: GroupPageComponent
    },
    {
        path: 'my-groups',
        component: MyGroupsComponent
    },
    {
        path: 'logout',
        component: LogoutComponent
    },
    {
        path: 'admin',
        component: AdminPanelComponent
    }
];
