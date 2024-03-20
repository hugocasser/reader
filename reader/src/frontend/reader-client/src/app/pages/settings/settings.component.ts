import { Component } from '@angular/core';
import { PageHeaderComponent } from '../../components/page-header/page-header.component';
import { MenuComponent } from '../../components/menu/menu.component';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-settings',
  standalone: true,
  imports: [PageHeaderComponent, MenuComponent, FormsModule, CommonModule],
  templateUrl: './settings.component.html',
  styleUrl: './settings.component.css'
})

export class SettingsComponent {
  title ='Settings'
  readingPageSize: number = parseInt(localStorage.getItem('readingPageSize') || '1000');
  pageSize: number = parseInt(localStorage.getItem('pageSize') || '12');
  symbolsSize: number = parseInt(localStorage.getItem('symbolsSize') || '10');
  isPageSizeWrong: boolean = false;
  isReadingPageSizeWrong: boolean = false;
  isSymbolsSizeWrong: boolean = false;
  sendNotesAutomatic: boolean = this.loadSendNotesAutomatic();

  onKeyPress(event: KeyboardEvent) {
    const charCode = event.charCode;
    if (charCode !== 0 && (charCode < 48 || charCode > 57) && charCode !== 46) {
      event.preventDefault();
    }
  }

  loadSendNotesAutomatic(): boolean{
    var value = localStorage.getItem('sendNotesAutomatic');

    if (value === null || value === undefined){
      return true;
    }

    if (value === 'true'){
      return true;
    }

    return false;
  }
  onPageSizeInputChange(event: Event){
    const input = event.target as HTMLInputElement;
    input.value = (Math.max(0, parseFloat(input.value) || 0)).toString();

    if (parseInt(input.value) > 52 || parseInt(input.value) < 8){
      this.isPageSizeWrong = true;
    }
    else{
      this.isPageSizeWrong = false;
    }
  }

  onReadingPageSizeInputChange(event: Event){
    const input = event.target as HTMLInputElement;
    input.value = (Math.max(0, parseFloat(input.value) || 0)).toString();

    if (parseInt(input.value) > 5000 || parseInt(input.value) < 100){
      this.isReadingPageSizeWrong = true;
    }
    else{
      this.isReadingPageSizeWrong = false;
    }
  }
  
  onSymbolsSizeInputChange(event: Event){
    const input = event.target as HTMLInputElement;
    input.value = (Math.max(0, parseFloat(input.value) || 0)).toString();

    if (parseInt(input.value) > 96 || parseInt(input.value) < 7){
      this.isSymbolsSizeWrong = true;
    }
    else{
      this.isSymbolsSizeWrong = false;
    }
  }

  saveSettings(event: Event){
    if (this.sendNotesAutomatic){
      localStorage.setItem('sendNotesAutomatic', 'true');
    }
    else{
      localStorage.setItem('sendNotesAutomatic', 'false');
    }

    localStorage.setItem('symbolsSize', this.symbolsSize.toString());
    localStorage.setItem('readingPageSize', this.readingPageSize.toString());
    localStorage.setItem('pageSize', this.pageSize.toString());
  }
}
