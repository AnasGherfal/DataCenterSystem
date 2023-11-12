import type { RepresentativeRequest } from "@/Modules/Representatives/RepresentativeRequest";


export interface VisitRequest {

    expectedStartTime: string;
    expectedEndTime: string;
    companions: any;
  
    visitType:number;
    subscriptionId: number|null;
    representatives:RepresentativeRequest[];
    notes: string;
}