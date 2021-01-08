export enum AccountStatus
{
    Available = 1,
    Working = 2,
    Restart = 3 
}

export enum BotSignalStatus
{
    Waiting = 1,
    InProgress = 2,
    Finished = 3 
}

export class AccountPowerToFly
{
    Id: number;
    Email: string;
    Password: string;
    Status: AccountStatus;
    SiteId: number;
}

export class BotSignal 
{
    Id: number;
    JobLink: string;
    FoundJob: number;
    ForResponseJob: number;
    SendedJob: number;
    Date: string;
    Status: BotSignalStatus;
    IsSelected: boolean;
}