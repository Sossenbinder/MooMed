export enum PopUpMessageLevel {
    Info,
    Warning,
    Error,
};

export interface PopUpNotification {
    Id: number;
    Message: string;
    MessageLevel: PopUpMessageLevel;
    Heading: string;
    TimeToLive: number;
}