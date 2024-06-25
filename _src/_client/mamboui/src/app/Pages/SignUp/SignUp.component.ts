import { SignUpPrivacyPolicyComponent } from './SignUpPrivacyPolicy/SignUpPrivacyPolicy.component';
import { CommonModule } from '@angular/common';
import {
  ChangeDetectionStrategy,
  ChangeDetectorRef,
  Component,
  inject,
} from '@angular/core';
import {
  AbstractControl,
  FormBuilder,
  ReactiveFormsModule,
  ValidationErrors,
  ValidatorFn,
} from '@angular/forms';
import { Router } from '@angular/router';
import { ButtonModule } from 'primeng/button';
import { FloatLabelModule } from 'primeng/floatlabel';
import { InputTextModule } from 'primeng/inputtext';
import { RippleModule } from 'primeng/ripple';
import { DialogService, DynamicDialogRef } from 'primeng/dynamicdialog';
import { DividerModule } from 'primeng/divider';

@Component({
  selector: 'app-sign-up',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FloatLabelModule,
    InputTextModule,
    ButtonModule,
    RippleModule,
    SignUpPrivacyPolicyComponent,
    DividerModule,
  ],
  providers: [DialogService],
  template: `
    <div class="grid grid-cols-24 mt-2">
      <div class="col-span-24 flex items-center justify-center">
        <img
          (click)="this.goToPage('/')"
          class="p-0 m-0 cursor-pointer transition ease-out hover:scale-110"
          height="100px"
          width="100px"
          src="/assets/logo/logo.png"
        />
      </div>
      <div class="col-span-24 text-center">
        <h2>Get started with Mambo</h2>
        <h4>No credit card required.</h4>
      </div>
      <form
        (ngSubmit)="this.onSignUpFormSubmitHandle($event)"
        [formGroup]="this.signUpForm"
        class="col-span-24 grid grid-cols-24 gap-y-5 mt-2"
      >
        <div class="col-span-9"></div>
        <p-floatLabel class="col-span-6">
          <input
            class="w-full"
            pInputText
            id="email"
            [name]="this.emailInputKey"
            [formControlName]="this.emailInputKey"
          />
          <label for="email">Email</label>
        </p-floatLabel>
        <div class="col-span-9"></div>
        <div class="col-span-24 text-center">
          @if (!this.isEmailInputValid) {
          <strong>{{ emailInputErrorMessage }}</strong>
          }
        </div>
        <div class="col-span-24 text-center">
          <small
            >I agree to the Mambo Agreement and acknowledge the
            <i
              (click)="openPrivacyPolicyDialog()"
              class="text-blue-500 cursor-pointer"
              >Privacy Policy</i
            >.</small
          >
        </div>
        <div class="col-span-9"></div>
        <div class="col-span-6">
          <button
            pButton
            pRipple
            label="Sign Up"
            class="p-button-primary w-full"
            type="submit"
          ></button>
        </div>
        <div class="col-span-9"></div>
        <div class="col-span-9"></div>
        <div class="col-span-6 text-center">
          <p-divider />
          <p>Or continue with</p>
        </div>
        <div class="col-span-9"></div>
      </form>
    </div>
  `,
  styleUrl: './SignUp.component.css',
  changeDetection: ChangeDetectionStrategy.OnPush,
})
export class SignUpComponent {
  private readonly router = inject(Router);
  private readonly dialogService = inject(DialogService);
  private dialogRef: DynamicDialogRef | undefined;

  public emailInputKey: string = 'email';

  private readonly _changeDetector = inject(ChangeDetectorRef);
  private readonly _formBuilder = inject(FormBuilder);
  public goToPage(route: string) {
    this.router.navigate([`${route}`]);
  }
  public signUpForm = this._formBuilder.group({
    [this.emailInputKey]: ['', this.emailInputValidator()],
  });
  get emailInputCurrentValue() {
    return this.signUpForm.controls[this.emailInputKey].value;
  }
  public resetEmailInputCurrentValue() {
    this.signUpForm.controls[this.emailInputKey].markAsUntouched();
    this.signUpForm.controls[this.emailInputKey].setValue('');
  }
  get isEmailInputValid() {
    return (
      !this.signUpForm.controls[this.emailInputKey].dirty ||
      this.signUpForm.controls[this.emailInputKey].errors?.['isValid']
    );
  }
  get emailInputErrorMessage() {
    return this.signUpForm.controls[this.emailInputKey].errors?.[
      'errorMessage'
    ];
  }
  get isSignUpFormValid(): boolean {
    return this.isEmailInputValid;
  }
  private emailInputValidator(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value;

      if (
        value == '' ||
        value == null ||
        value == undefined ||
        typeof value != 'string'
      )
        return {
          isValid: false,
          errorMessage: 'This field is required',
        };
      return null;
    };
  }
  public onSignUpFormSubmitHandle(event: any) {
    event.preventDefault();
    //var formData1: any = new FormData();
    console.log(this.signUpForm);
  }

  public handleSignUpFormApply() {
    Object.keys(this.signUpForm.controls).forEach((field) => {
      const control = this.signUpForm.get(field);
      control?.markAsTouched({ onlySelf: true });
    });
    if (this.signUpForm.valid) {
      //this.submitFormButton?.nativeElement.click()
      alert('FORM DOGRU');
    }
  }
  public openPrivacyPolicyDialog() {
    this.dialogRef = this.dialogService.open(SignUpPrivacyPolicyComponent, {
      header: 'Terms and Conditions',
      closable: true,
      draggable: true,
      duplicate: false,
      width: '80%',
      closeOnEscape: true,
      focusOnShow: false,
    });
  }
}
