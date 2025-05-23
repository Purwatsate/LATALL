import { Component, effect, signal } from '@angular/core';
import { ChangeDetectionStrategy, inject } from '@angular/core';
import { toSignal } from '@angular/core/rxjs-interop';
import {
  FormBuilder,
  FormControl,
  FormsModule,
  ReactiveFormsModule,
} from '@angular/forms';
import {
  FloatLabelType,
  MatFormFieldModule,
} from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { map } from 'rxjs/operators';
import { AuthService } from '../../../core/services/auth/auth.service';
import { Credentials } from '../../models/auth.model';

@Component({
  selector: 'app-login',
  imports: [
    FormsModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.scss',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class LoginComponent {
  readonly hideRequiredControl = new FormControl(false);

  readonly floatLabelControl = new FormControl('auto' as FloatLabelType);
  readonly loginForm = inject(FormBuilder).group({
    username: new FormControl('user'),
    password: new FormControl('1qaz!QAZ2wsx@WSX!'),
    hideRequired: this.hideRequiredControl,
    floatLabel: this.floatLabelControl,
  });
  protected readonly hideRequired = toSignal(
    this.hideRequiredControl.valueChanges
  );
  protected readonly floatLabel = toSignal(
    this.floatLabelControl.valueChanges.pipe(map((v) => v || 'auto')),
    { initialValue: 'auto' }
  );

  private authService = inject(AuthService);
  isLoading = signal(false);

  constructor() {}

  ngOnInit() {
    console.log('called login component')
  }

  onLoginClick() {
    this.isLoading.set(true);
    const creditial: Credentials = {
      username: this.loginForm.get('username')?.value ?? '',
      password: this.loginForm.get('password')?.value ?? '',
    };
    this.authService.login(creditial).subscribe({
      next: () => {
        this.isLoading.set(false);
      },
      error: (error) => {
        this.isLoading.set(false);
        console.log(error);
      },
    });
  }
}
