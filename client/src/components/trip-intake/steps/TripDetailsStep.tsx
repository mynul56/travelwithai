"use client";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { useTripIntakeStore } from "@/store/useTripIntakeStore";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { Select, SelectContent, SelectItem, SelectTrigger, SelectValue } from "@/components/ui/select";
import { ArrowLeft, ArrowRight } from "lucide-react";
import { useEffect } from "react";

const tripDetailsSchema = z.object({
  destination: z.string().min(2, "Destination is required"),
  travelers: z.coerce.number().min(1, "At least 1 traveler required").max(20, "Maximum 20 travelers allowed"),
  budget: z.enum(["Budget", "Moderate", "Luxury"]),
  // For simplicity in scaffolding, using string dates, but normally we'd use a Popover+Calendar
  startDate: z.string().min(1, "Start Date is required"),
  endDate: z.string().min(1, "End Date is required"),
});

type TripDetailsValues = z.infer<typeof tripDetailsSchema>;

export function TripDetailsStep() {
  const store = useTripIntakeStore();
  
  const form = useForm<TripDetailsValues>({
    resolver: zodResolver(tripDetailsSchema) as any,
    defaultValues: {
      destination: store.destination,
      travelers: store.travelers,
      budget: store.budget as any,
      startDate: store.startDate || "",
      endDate: store.endDate || "",
    },
  });

  useEffect(() => {
    form.reset({
      destination: store.destination,
      travelers: store.travelers,
      budget: store.budget as any,
      startDate: store.startDate || "",
      endDate: store.endDate || "",
    });
  }, [store.destination, store.travelers, store.budget, store.startDate, store.endDate, form]);

  const onSubmit = (data: TripDetailsValues) => {
    store.updateField("destination", data.destination);
    store.updateField("travelers", data.travelers);
    store.updateField("budget", data.budget);
    store.updateField("startDate", data.startDate);
    store.updateField("endDate", data.endDate);
    store.nextStep();
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6 animate-in fade-in slide-in-from-right-4 duration-500">
        
        <FormField
          control={form.control}
          name="destination"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Where do you want to go?</FormLabel>
              <FormControl>
                <Input placeholder="e.g. Paris, France or 'Anywhere'" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <FormField
            control={form.control}
            name="startDate"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Start Date</FormLabel>
                <FormControl>
                  <Input type="date" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
          <FormField
            control={form.control}
            name="endDate"
            render={({ field }) => (
              <FormItem>
                <FormLabel>End Date</FormLabel>
                <FormControl>
                  <Input type="date" {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>

        <div className="grid grid-cols-1 md:grid-cols-2 gap-6">
          <FormField
            control={form.control}
            name="travelers"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Number of Travelers</FormLabel>
                <FormControl>
                  <Input type="number" min={1} max={20} {...field} />
                </FormControl>
                <FormMessage />
              </FormItem>
            )}
          />

          <FormField
            control={form.control}
            name="budget"
            render={({ field }) => (
              <FormItem>
                <FormLabel>Budget Tier</FormLabel>
                <Select onValueChange={field.onChange} defaultValue={field.value}>
                  <FormControl>
                    <SelectTrigger>
                      <SelectValue placeholder="Select a budget tier" />
                    </SelectTrigger>
                  </FormControl>
                  <SelectContent>
                    <SelectItem value="Budget">Budget</SelectItem>
                    <SelectItem value="Moderate">Moderate</SelectItem>
                    <SelectItem value="Luxury">Luxury</SelectItem>
                  </SelectContent>
                </Select>
                <FormMessage />
              </FormItem>
            )}
          />
        </div>

        <div className="flex justify-between pt-4">
          <Button type="button" variant="outline" onClick={store.prevStep} className="gap-2">
            <ArrowLeft className="w-4 h-4" /> Back
          </Button>
          <Button type="submit" className="gap-2">
            Continue <ArrowRight className="w-4 h-4" />
          </Button>
        </div>
      </form>
    </Form>
  );
}
