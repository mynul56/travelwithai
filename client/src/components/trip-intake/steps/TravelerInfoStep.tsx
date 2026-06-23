"use client";

import { useForm } from "react-hook-form";
import { zodResolver } from "@hookform/resolvers/zod";
import * as z from "zod";
import { useTripIntakeStore } from "@/store/useTripIntakeStore";
import { Form, FormControl, FormField, FormItem, FormLabel, FormMessage } from "@/components/ui/form";
import { Input } from "@/components/ui/input";
import { Button } from "@/components/ui/button";
import { ArrowRight } from "lucide-react";
import { useEffect } from "react";

const travelerInfoSchema = z.object({
  fullName: z.string().min(2, "Full name must be at least 2 characters"),
  email: z.string().email("Invalid email address"),
  phone: z.string().min(10, "Phone number should be at least 10 digits").optional().or(z.literal('')),
});

type TravelerInfoValues = z.infer<typeof travelerInfoSchema>;

export function TravelerInfoStep() {
  const store = useTripIntakeStore();
  
  const form = useForm<TravelerInfoValues>({
    resolver: zodResolver(travelerInfoSchema),
    defaultValues: {
      fullName: store.fullName,
      email: store.email,
      phone: store.phone,
    },
  });

  // Keep form in sync with Zustand if draft loads slowly
  useEffect(() => {
    form.reset({
      fullName: store.fullName,
      email: store.email,
      phone: store.phone,
    });
  }, [store.fullName, store.email, store.phone, form]);

  const onSubmit = (data: TravelerInfoValues) => {
    store.updateField("fullName", data.fullName);
    store.updateField("email", data.email);
    store.updateField("phone", data.phone);
    store.nextStep();
  };

  return (
    <Form {...form}>
      <form onSubmit={form.handleSubmit(onSubmit)} className="space-y-6 animate-in fade-in slide-in-from-bottom-4 duration-500">
        <FormField
          control={form.control}
          name="fullName"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Full Name</FormLabel>
              <FormControl>
                <Input placeholder="John Doe" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="email"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Email Address</FormLabel>
              <FormControl>
                <Input type="email" placeholder="john@example.com" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        <FormField
          control={form.control}
          name="phone"
          render={({ field }) => (
            <FormItem>
              <FormLabel>Phone Number (Optional)</FormLabel>
              <FormControl>
                <Input type="tel" placeholder="+1 (555) 000-0000" {...field} />
              </FormControl>
              <FormMessage />
            </FormItem>
          )}
        />
        
        <div className="flex justify-end pt-4">
          <Button type="submit" className="gap-2">
            Continue <ArrowRight className="w-4 h-4" />
          </Button>
        </div>
      </form>
    </Form>
  );
}
