import {Component, OnInit, Input} from '@angular/core';
import {NgbDateStruct} from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'app-datepicker',
  templateUrl: './datepicker.component.html'
})

export class Datepicker implements OnInit {
  model!: NgbDateStruct;

  @Input() placeholder!: string;

  constructor() { }

  ngOnInit() {
  }
}