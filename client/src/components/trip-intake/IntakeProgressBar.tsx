import { useTripIntakeStore } from "@/store/useTripIntakeStore";
import { Check } from "lucide-react";

const steps = [
  { id: 1, name: "Traveler Info" },
  { id: 2, name: "Trip Details" },
  { id: 3, name: "Travel Style" },
  { id: 4, name: "Preferences" },
  { id: 5, name: "Special Needs" },
  { id: 6, name: "Review" },
];

export function IntakeProgressBar() {
  const currentStep = useTripIntakeStore((state) => state.currentStep);

  return (
    <div className="w-full mb-8">
      <div className="flex items-center justify-between">
        {steps.map((step, index) => {
          const isCompleted = currentStep > step.id;
          const isCurrent = currentStep === step.id;

          return (
            <div key={step.id} className="flex flex-col items-center relative z-10">
              <div
                className={`w-10 h-10 rounded-full flex items-center justify-center border-2 transition-colors duration-300 ${
                  isCompleted
                    ? "bg-primary border-primary text-primary-foreground"
                    : isCurrent
                    ? "bg-background border-primary text-primary"
                    : "bg-background border-muted text-muted-foreground"
                }`}
              >
                {isCompleted ? <Check className="w-5 h-5" /> : <span>{step.id}</span>}
              </div>
              <span
                className={`text-xs mt-2 hidden sm:block font-medium ${
                  isCurrent ? "text-primary" : "text-muted-foreground"
                }`}
              >
                {step.name}
              </span>
            </div>
          );
        })}
      </div>
      <div className="relative mt-[-2.3rem] sm:mt-[-3.3rem] mb-6 h-1 bg-muted z-0 w-[calc(100%-2.5rem)] mx-auto">
        <div
          className="absolute top-0 left-0 h-full bg-primary transition-all duration-300"
          style={{ width: `${((currentStep - 1) / (steps.length - 1)) * 100}%` }}
        />
      </div>
    </div>
  );
}
