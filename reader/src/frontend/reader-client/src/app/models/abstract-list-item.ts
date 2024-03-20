export interface AbstractItem{
    name: string;
    id : string;
    type: string;
    info: string;
    elementId: number;

    /**
     * name
     */
    toInfo() : void;
}