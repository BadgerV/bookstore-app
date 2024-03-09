import { Component, OnInit } from '@angular/core';
import { FooterComponent } from '../../../shared/footer/footer.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { AppState } from '../../../store/reducers/reducers';
import { Store } from '@ngrx/store';
import { login, logout } from '../../../store/actions/auth.actions';
import { AuthService } from '../../../core/services/authService';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FooterComponent, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent implements OnInit {
  myForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private store: Store<AppState>,
    private authService: AuthService
  ) {
    this.myForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }
  ngOnInit(): void {
    this.store.dispatch(logout());
    this.authService.navigateToHomepage();
  }

  onSubmit() {
    if (this.myForm.valid) {
      console.log(this.myForm.value);
      this.store.dispatch(login(this.myForm.value));
    }
  }
}
