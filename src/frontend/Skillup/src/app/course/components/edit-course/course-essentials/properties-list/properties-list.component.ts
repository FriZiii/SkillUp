import { Component, input, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';

@Component({
  selector: 'app-properties-list',
  standalone: true,
  imports: [ButtonModule, FloatLabelModule, FormsModule, InputTextModule],
  templateUrl: './properties-list.component.html',
  styleUrl: './properties-list.component.css'
})
export class PropertiesListComponent {
  items = input.required<string[]>();
  inputPlaceHolder = input.required<string>();
  itemAdded = output<string>();
  itemRemoved = output<string>();
  newItem = signal('');
  

  addItem(itemToAdd: string){
    this.itemAdded.emit(itemToAdd);
    this.newItem.set('');
  }

  removeItem(itemToRemove: string){
    this.itemRemoved.emit(itemToRemove);
  }
}
