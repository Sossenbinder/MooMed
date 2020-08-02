export enum PopUpMessageLevel {
    Info,
    Warning,
    Error,
};

export type PopUpNotification = {
    message: string;
    messageLevel: PopUpMessageLevel;
    heading?: string;
    timeToLive?: number;
}