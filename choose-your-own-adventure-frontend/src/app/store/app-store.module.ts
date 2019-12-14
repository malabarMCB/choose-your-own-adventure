import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {ActionReducer, MetaReducer, StoreModule} from '@ngrx/store';
import {localStorageSync} from 'ngrx-store-localstorage';
import {environment} from '../../environments/environment';
import {StoreDevtoolsModule} from '@ngrx/store-devtools';
import {appReducer} from './app-store.reducer';

export function localStorageSyncReducer(reducer: ActionReducer<any>): ActionReducer<any> {
  return localStorageSync({keys: ['state'], rehydrate: true})(reducer);
}
export const metaReducers: Array<MetaReducer<any, any>> = [localStorageSyncReducer];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    StoreModule.forRoot({state: appReducer}, {metaReducers}),
    StoreDevtoolsModule.instrument({
      maxAge: 25,
      logOnly: environment.production,
    }),
    StoreDevtoolsModule.instrument({ maxAge: 25, logOnly: environment.production }),
  ]
})
export class AppStoreModule { }
