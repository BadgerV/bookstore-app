import { Component } from '@angular/core';
import { FooterComponent } from '../../../shared/footer/footer.component';
import {
  FormBuilder,
  FormGroup,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { RouterLink } from '@angular/router';
import { ApiService } from '../../../core/services/apiService';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [FooterComponent, ReactiveFormsModule, RouterLink],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
})
export class LoginComponent {
  myForm!: FormGroup;

  constructor(
    private formBuilder: FormBuilder,
    private apiService: ApiService
  ) {
    this.myForm = this.formBuilder.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.myForm.valid) {
      this.apiService.login(this.myForm.value).subscribe((result) => {
        console.log(result);
      });
    }
  }
}
