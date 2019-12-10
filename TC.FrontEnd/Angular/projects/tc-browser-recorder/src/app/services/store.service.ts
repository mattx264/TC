import { Injectable } from '@angular/core';
import { ProjectViewModel } from '../../../../shared/src/lib/models/project/projectViewModel';
import { Observable, BehaviorSubject } from 'rxjs';
import { OperatorModel } from '../../../../shared/src/lib/models/operatorModel';

@Injectable({
  providedIn: 'root'
})
export class StoreService {
  
  state$: BehaviorSubject<ProjectViewModel>;
  stateOperatorsData$: BehaviorSubject<OperatorModel[]>;
  constructor() {
    this.state$ = new BehaviorSubject<ProjectViewModel>(null);
    this.stateOperatorsData$ = new BehaviorSubject<OperatorModel[]>(null);
  }

  private _project: ProjectViewModel;
  getValue(): ProjectViewModel {
    return this.state$.getValue();
  }
  setValue(nextState: ProjectViewModel): void {
    this.state$.next(nextState);
  }
  setOperatorsData(operatorsData: OperatorModel[]) {
    this.stateOperatorsData$.next(operatorsData);
  }
  getOperatorsData() : OperatorModel[]{
    return this.stateOperatorsData$.getValue();
  }
}
