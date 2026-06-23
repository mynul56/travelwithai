import Link from "next/link";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";
import { Compass, ArrowRight } from "lucide-react";

export default function RegisterPage() {
  return (
    <div className="min-h-screen flex items-center justify-center bg-black relative overflow-hidden py-12">
      {/* Background styling matching the landing page */}
      <div className="absolute inset-0 bg-[url('https://images.unsplash.com/photo-1469854523086-cc02fe5d8800?q=80&w=2921&auto=format&fit=crop')] bg-cover bg-center opacity-20 mix-blend-overlay"></div>
      <div className="absolute inset-0 bg-gradient-to-t from-black via-black/80 to-transparent"></div>
      
      <div className="relative z-10 w-full max-w-md p-8 rounded-3xl bg-white/5 border border-white/10 backdrop-blur-md shadow-2xl animate-in fade-in zoom-in-95 duration-500">
        <div className="flex flex-col items-center mb-8">
          <Link href="/" className="flex items-center gap-2 mb-6">
            <Compass className="h-8 w-8 text-primary" />
            <span className="text-2xl font-bold tracking-tighter text-white">WanderAI</span>
          </Link>
          <h1 className="text-3xl font-bold text-white mb-2">Create an Account</h1>
          <p className="text-white/60 text-center text-sm">Join WanderAI to instantly generate and save your perfect travel itineraries.</p>
        </div>

        <form className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="name" className="text-white/80">Full Name</Label>
            <Input 
              id="name" 
              type="text" 
              placeholder="John Doe" 
              className="bg-black/50 border-white/10 text-white placeholder:text-white/30 focus-visible:ring-primary h-12"
            />
          </div>
          <div className="space-y-2">
            <Label htmlFor="email" className="text-white/80">Email address</Label>
            <Input 
              id="email" 
              type="email" 
              placeholder="you@example.com" 
              className="bg-black/50 border-white/10 text-white placeholder:text-white/30 focus-visible:ring-primary h-12"
            />
          </div>
          <div className="space-y-2">
            <Label htmlFor="password" className="text-white/80">Password</Label>
            <Input 
              id="password" 
              type="password" 
              placeholder="••••••••" 
              className="bg-black/50 border-white/10 text-white placeholder:text-white/30 focus-visible:ring-primary h-12"
            />
          </div>
          
          <Button type="button" className="w-full h-12 mt-6 bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-500 hover:to-purple-500 text-white border-0 shadow-lg shadow-purple-500/25 transition-all text-base font-semibold rounded-full">
            Sign Up <ArrowRight className="ml-2 h-4 w-4" />
          </Button>
        </form>

        <div className="mt-8 text-center text-sm text-white/60">
          Already have an account?{" "}
          <Link href="/login" className="text-white hover:text-primary transition-colors font-medium underline underline-offset-4">
            Log in
          </Link>
        </div>
      </div>
    </div>
  );
}
