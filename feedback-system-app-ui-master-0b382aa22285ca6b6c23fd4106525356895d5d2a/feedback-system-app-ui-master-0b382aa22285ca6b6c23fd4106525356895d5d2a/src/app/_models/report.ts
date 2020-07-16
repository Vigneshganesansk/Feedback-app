import { POCModel } from './pocModel';


export class Report {
    EventID: string;
    EventName: string;
    EventDescription: string;
    EventDate: string;
    Month: string;
    BaseLocation: string;	
    BeneficiaryName: string;
    VenueAddress: string;
    CouncilName: string;
    Project: string;
    Category: string;
    TotalVolunteers: number;
    TotalVolunteerHours: number;
    TotalTravelHours: number;
    OverallVolunteeringHours: number;
    LivesImpacted: number;
    ActivityType: string;
    Status: string;
    POCID: number;
    POCName: string;
    POCContact: number;
    PocList: POCModel[];
    ParticipantList: any[];
    UnregistedPartyList: any[];
    NotParticipatedList: any[];
    AverageRating: number;
    BusinessUnit: string;

    ParticipatedFbCount: number;
    NotParticipatedFbCount: number;
    UnregusteresFbCount: number;
}
