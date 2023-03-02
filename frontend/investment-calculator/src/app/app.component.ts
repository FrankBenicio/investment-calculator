import { Component } from '@angular/core';
import { AppService } from './app.service';
import {
  AbstractControl,
  FormBuilder,
  FormGroup,
  FormControl,
  Validators,
} from '@angular/forms';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  cdbResponse: any;
  form: FormGroup = new FormGroup({
    investmentValue: new FormControl(''),
    month: new FormControl(''),
  });
  submitted = false;

  constructor(private formBuilder: FormBuilder, private AppService: AppService) { }

  ngOnInit(): void {
    this.form = this.formBuilder.group(
      {
        investmentValue: ['', Validators.required],
        month: ['', Validators.required]
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
      .subscribe((data) => { this.cdbResponse = data });
  }
}
