import { ValidatorFn, ValidationErrors, AbstractControl } from '@angular/forms';
import { AppSettings } from 'src/AppSettings';

export function UUID_Validator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    let value: string = control.value
    return AppSettings.UUID_REGEX.test(value) ? null : { invalidUUID: { value: value } };
  };
}