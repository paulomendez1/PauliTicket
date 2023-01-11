import { Component, EventEmitter, Input, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { userCredentials } from 'src/app/models/security';

@Component({
  selector: 'app-auth-form',
  templateUrl: './auth-form.component.html',
  styleUrls: ['./auth-form.component.css']
})
export class AuthFormComponent {
  constructor(private formBuilder: FormBuilder) { }

  form!: FormGroup

  @Input()
  action: string = 'Register'

  @Output()
  onSubmit = new EventEmitter<userCredentials>();

  ngOnInit(): void {
    if (this.action !== 'Register') {
      this.form = this.formBuilder.group({
        email: ['', {
          validators: [Validators.required, Validators.email]
        }],
        password: ['', {
          validators: [Validators.required]
        }]
      })
    }
    else {
      this.form = this.formBuilder.group({
        email: ['', {
          validators: [Validators.required, Validators.email]
        }],
        username: ['', {
          validators: [Validators.required]
        }],
        firstName: ['', {
          validators: [Validators.required]
        }],
        lastName: ['', {
          validators: [Validators.required]
        }],
        password: ['', {
          validators: [Validators.required]
        }]
      })
    }
  }
  getEmailErrorMsg() {
    var field = this.form.get('email')
    if (field?.hasError('required')) {
      return "The email field is required";
    }
    if (field?.hasError('email')) {
      return "The email format is invalid";
    }
    return ''
  }
}
