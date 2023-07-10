import { AbstractControl } from '@angular/forms';

export class AppValidatorFn {

  public static oordiateValidator() {
    return (control: AbstractControl) => {
      let coor = control.value;
      if (!coor) return null;
      else {
        var toado = coor.split(', ');
        if (toado.length == 2 &&
            isFinite(toado[0]) &&
            Math.abs(toado[0]) <= 90 && //valid Long
            isFinite(toado[1]) &&
            Math.abs(toado[1]) <= 180
         ) return { coordiateInvalid: false };
        else return { coordiateInvalid: true };
      }
    };
  }

}
