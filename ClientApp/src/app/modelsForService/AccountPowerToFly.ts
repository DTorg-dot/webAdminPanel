export enum AccountStatus
{
    Available = 1,
    Working = 2,
    Restart = 3 
}

export class AccountPowerToFly
{
    Id: number;
    Email: string;
    Password: string;
    Status: AccountStatus;
    SiteId: number;
}