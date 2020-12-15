import { AccountStatus } from "src/app/modelsForService/accountPowerToFly";

export class Account {

    constructor (public Email: string,
        public Password: string,
        public IsSelected: boolean,
        public Status: AccountStatus) {}
}