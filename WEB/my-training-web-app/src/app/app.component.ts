import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import {MatIconModule} from '@angular/material/icon';
import {MatDividerModule} from '@angular/material/divider';
import {MatButtonModule} from '@angular/material/button';
import {ChangeDetectionStrategy, inject} from '@angular/core';
import {toSignal} from '@angular/core/rxjs-interop';
import {FormBuilder, FormControl, FormsModule, ReactiveFormsModule} from '@angular/forms';
import {MatCheckboxModule} from '@angular/material/checkbox';
import {FloatLabelType, MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import {MatRadioModule} from '@angular/material/radio';
import {MatSelectModule} from '@angular/material/select';
import {map} from 'rxjs/operators';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, MatSlideToggleModule,MatButtonModule, MatDividerModule, MatIconModule,
    FormsModule,
    ReactiveFormsModule,
    MatCheckboxModule,
    MatRadioModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatIconModule,
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  title = 'testing 1 2 3';
    readonly hideRequiredControl = new FormControl(false);
  readonly floatLabelControl = new FormControl('auto' as FloatLabelType);
  readonly options = inject(FormBuilder).group({
    hideRequired: this.hideRequiredControl,
    floatLabel: this.floatLabelControl,
  });
  protected readonly hideRequired = toSignal(this.hideRequiredControl.valueChanges);
  protected readonly floatLabel = toSignal(
    this.floatLabelControl.valueChanges.pipe(map(v => v || 'auto')),
    {initialValue: 'auto'},
  );
}
