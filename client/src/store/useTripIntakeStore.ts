import { create } from 'zustand';
import { persist, createJSONStorage } from 'zustand/middleware';

export interface TripIntakeState {
  // Step 1: Traveler Info
  fullName: string;
  email: string;
  phone: string;

  // Step 2: Trip Details
  destination: string;
  startDate: string | null;
  endDate: string | null;
  travelers: number;
  budget: string;

  // Step 3: Travel Style
  styles: string[];

  // Step 4: Preferences
  preferences: string[];

  // Step 5: Special Needs
  specialNeeds: string[];

  // Form State
  currentStep: number;
  isSubmitting: boolean;

  // Actions
  updateField: (field: keyof TripIntakeState, value: any) => void;
  nextStep: () => void;
  prevStep: () => void;
  setSubmitting: (submitting: boolean) => void;
  resetForm: () => void;
}

const initialState = {
  fullName: '',
  email: '',
  phone: '',
  destination: '',
  startDate: null,
  endDate: null,
  travelers: 1,
  budget: 'Moderate',
  styles: [],
  preferences: [],
  specialNeeds: [],
  currentStep: 1,
  isSubmitting: false,
};

export const useTripIntakeStore = create<TripIntakeState>()(
  persist(
    (set) => ({
      ...initialState,
      updateField: (field, value) => set((state) => ({ ...state, [field]: value })),
      nextStep: () => set((state) => ({ currentStep: Math.min(state.currentStep + 1, 6) })),
      prevStep: () => set((state) => ({ currentStep: Math.max(state.currentStep - 1, 1) })),
      setSubmitting: (submitting) => set({ isSubmitting: submitting }),
      resetForm: () => set(initialState),
    }),
    {
      name: 'trip-intake-draft', // unique name
      storage: createJSONStorage(() => localStorage), // (optional) by default, 'localStorage' is used
      partialize: (state) => 
        Object.fromEntries(
          Object.entries(state).filter(([key]) => key !== 'isSubmitting')
        ), // Don't persist UI transient state
    }
  )
);
