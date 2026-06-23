"use client";

import { useTripIntakeStore } from "@/store/useTripIntakeStore";
import { IntakeProgressBar } from "./IntakeProgressBar";
import { Card, CardContent, CardDescription, CardHeader, CardTitle } from "@/components/ui/card";
import { Button } from "@/components/ui/button";
import { TravelerInfoStep } from "./steps/TravelerInfoStep";
import { TripDetailsStep } from "./steps/TripDetailsStep";

export function TripIntakeWizard() {
  const currentStep = useTripIntakeStore((state) => state.currentStep);
  const resetForm = useTripIntakeStore((state) => state.resetForm);

  const renderStep = () => {
    switch (currentStep) {
      case 1:
        return <TravelerInfoStep />;
      case 2:
        return <TripDetailsStep />;
      case 3:
        return <div>Travel Style Step (To be implemented)</div>;
      case 4:
        return <div>Preferences Step (To be implemented)</div>;
      case 5:
        return <div>Special Needs Step (To be implemented)</div>;
      case 6:
        return <div>Review & Submit Step (To be implemented)</div>;
      default:
        return null;
    }
  };

  return (
    <div className="max-w-3xl mx-auto py-10 px-4 sm:px-6 lg:px-8">
      <div className="mb-8 text-center">
        <h1 className="text-4xl font-extrabold tracking-tight text-primary sm:text-5xl">
          Plan Your Adventure
        </h1>
        <p className="mt-4 text-lg text-muted-foreground">
          Tell us about your dream trip, and our AI will craft the perfect itinerary.
        </p>
      </div>

      <IntakeProgressBar />

      <Card className="shadow-lg border-primary/10">
        <CardHeader>
          <div className="flex justify-between items-center">
            <div>
              <CardTitle className="text-2xl">Step {currentStep}</CardTitle>
              <CardDescription>
                {currentStep === 1 && "Basic contact information so we can reach you."}
                {currentStep === 2 && "Where and when are you traveling?"}
                {currentStep === 3 && "What is your preferred travel style?"}
                {currentStep === 4 && "Select your core preferences and activities."}
                {currentStep === 5 && "Any special accommodations we should know about?"}
                {currentStep === 6 && "Review your trip request before submitting."}
              </CardDescription>
            </div>
            <Button variant="ghost" size="sm" onClick={resetForm} className="text-muted-foreground hover:text-destructive">
              Clear Draft
            </Button>
          </div>
        </CardHeader>
        <CardContent>
          <div className="min-h-[300px]">
            {renderStep()}
          </div>
        </CardContent>
      </Card>
    </div>
  );
}
