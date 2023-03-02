import { Component } from '@angular/core';
import { AppService } from './app.service';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';

import { HttpResponse } from '@angular/common/http';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {

  form: FormGroup = new FormGroup({
    investmentValue: new FormControl(''),
    month: new FormControl(''),
  });
  submitted = false;

  grossTotalAmount: number = 0;
  amountInvested: number= 0;
  interestAmount: number= 0;
  netTotalAmount: number= 0;

  constructor(private formBuilder: FormBuilder, private AppService: AppService) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        investmentValue: ['', Validators.required],
        month: ['', [Validators.min(2), Validators.required]]
      },
    );
  }

  get f(): { [key: string]: AbstractControl } {
    return this.form.controls;
  }

  onSubmit(): void {
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    console.log(JSON.stringify(this.form.value, null, 2));
    this.AppService.postData(this.form.value)
      .subscribe((data) => { 
        this.grossTotalAmount = data.grossTotalAmount; 
        this.amountInvested = data.amountInvested; 
        this.interestAmount = data.interestAmount; 
        this.netTotalAmount = data.netTotalAmount; 
      
        console.log(this.grossTotalAmount);
      });
  }
}
