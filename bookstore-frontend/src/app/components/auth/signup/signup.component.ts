import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '../../../shared/footer/footer.component';
import {
  FormGroup,
  FormBuilder,
  Validators,
  FormsModule,
} from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AppState } from '../../../store/reducers/reducers';
import { Store } from '@ngrx/store';
import { logout } from '../../../store/actions/auth.actions';

@Component({
  selector: 'app-signup',
  standalone: true,
  imports: [FooterComponent, ReactiveFormsModule, RouterLink],
  templateUrl: './signup.component.html',
  styleUrl: './signup.component.scss',
})
export class SignupComponent implements OnInit {
  myForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store<AppState>
  ) {
    this.myForm = this.formBuilder.group({
      username: ['', Validators.required],
      email: ['', Validators.required],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required],
    });
  }
  ngOnInit(): void {
    this.store.dispatch(logout());
  }

  onsubmit() {
    console.log(this.myForm);

    if (this.myForm.valid) {
    }
  }
}
