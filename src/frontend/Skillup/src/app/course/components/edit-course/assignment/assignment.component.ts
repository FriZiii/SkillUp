import { Component, inject, input, OnInit } from '@angular/core';
import { AssetService } from '../../../services/asset.service';

@Component({
  selector: 'app-assignment',
  standalone: true,
  imports: [],
  templateUrl: './assignment.component.html',
  styleUrl: './assignment.component.css'
})
export class AssignmentComponent implements OnInit{
  //FromUrl
  elementId = input.required<string>();

  //Services
  asssetService = inject(AssetService);

  //Variables
  

  ngOnInit(): void {
    throw new Error('Method not implemented.');
  }

}
