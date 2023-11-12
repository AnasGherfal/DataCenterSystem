export interface RepresentativeRequest {
    id: string;
    firstName: string;
    lastName: string;
    identityNo: string;
    email: string;
    phoneNo: string;
    identityType: number | null;
    representationDocument: File | null; // Expecting a File type
    identityDocument: File | null; // Expecting a File type
    type: number | null;
    from: string;
    to: string;
  }
  
  