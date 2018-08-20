import {AbstractControl, ValidatorFn} from '@angular/forms';

export function matchPassword(): ValidatorFn {
  return (control: AbstractControl): {[key: string]: any} => {
    const formGroup = control.parent;
    if (formGroup) {
      const password = formGroup.get('password');
      return password.value !== control.value ? {'no-match': {value: control.value}} : null;
    }
    return null;
  };
}
