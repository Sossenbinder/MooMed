export enum PopUpMessageLevel {
    Info,
    Warning,
    Error,
};

export interface PopUpNotification {
    message: string;
    messageLevel: PopUpMessageLevel;
    heading: string;
    timeToLive: number;
}