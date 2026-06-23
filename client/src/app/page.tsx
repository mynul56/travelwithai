import Link from "next/link";
import { Button } from "@/components/ui/button";
import { ArrowRight, Compass, Map, Sparkles, Sun, Tent } from "lucide-react";

export default function Home() {
  return (
    <div className="flex flex-col min-h-screen bg-black text-white selection:bg-primary/30">
      {/* Navbar */}
      <header className="sticky top-0 z-50 w-full border-b border-white/10 bg-black/50 backdrop-blur-md">
        <div className="container mx-auto flex h-16 items-center justify-between px-4 md:px-6">
          <Link href="/" className="flex items-center gap-2">
            <Compass className="h-6 w-6 text-primary" />
            <span className="text-xl font-bold tracking-tighter text-white">WanderAI</span>
          </Link>
          <nav className="hidden md:flex gap-6">
            <Link href="#features" className="text-sm font-medium text-white/70 hover:text-white transition-colors">Features</Link>
            <Link href="#how-it-works" className="text-sm font-medium text-white/70 hover:text-white transition-colors">How it Works</Link>
          </nav>
          <div className="flex items-center gap-4">
            <Link href="/login" className="text-sm font-medium text-white/70 hover:text-white transition-colors hidden sm:block">Log in</Link>
            <Link href="/plan">
              <Button className="bg-white text-black hover:bg-white/90 rounded-full font-semibold px-6">
                Plan a Trip
              </Button>
            </Link>
          </div>
        </div>
      </header>

      <main className="flex-1">
        {/* Hero Section */}
        <section className="relative overflow-hidden pt-24 pb-32 md:pt-32 md:pb-48 lg:pt-40 lg:pb-56">
          <div className="absolute inset-0 bg-[url('https://images.unsplash.com/photo-1469854523086-cc02fe5d8800?q=80&w=2921&auto=format&fit=crop')] bg-cover bg-center opacity-40 mix-blend-overlay"></div>
          <div className="absolute inset-0 bg-gradient-to-t from-black via-black/80 to-transparent"></div>
          <div className="absolute inset-0 bg-gradient-to-r from-black via-black/50 to-transparent"></div>
          
          <div className="container relative z-10 mx-auto px-4 md:px-6 flex flex-col items-start text-left max-w-4xl">
            <div className="inline-flex items-center rounded-full border border-white/20 bg-white/10 px-3 py-1 text-sm font-medium text-white backdrop-blur-md mb-6 animate-fade-in">
              <Sparkles className="mr-2 h-4 w-4 text-amber-400" />
              <span>Powered by GPT-4o Architecture</span>
            </div>
            
            <h1 className="text-5xl md:text-7xl lg:text-8xl font-extrabold tracking-tight text-white mb-6 leading-[1.1] animate-in slide-in-from-bottom-8 duration-700">
              Your Next Great <br />
              <span className="text-transparent bg-clip-text bg-gradient-to-r from-blue-400 via-indigo-400 to-purple-400">Adventure</span> Awaits.
            </h1>
            
            <p className="max-w-[600px] text-lg md:text-xl text-white/70 mb-10 leading-relaxed animate-in slide-in-from-bottom-10 duration-1000">
              Stop stressing over spreadsheets. Our AI analyzes millions of data points, weather patterns, and local secrets to instantly craft your perfect, personalized itinerary.
            </p>
            
            <div className="flex flex-col sm:flex-row gap-4 animate-in slide-in-from-bottom-12 duration-1000">
              <Link href="/plan">
                <Button size="lg" className="h-14 px-8 text-lg rounded-full bg-gradient-to-r from-blue-600 to-purple-600 hover:from-blue-500 hover:to-purple-500 text-white border-0 shadow-lg shadow-purple-500/25 transition-all hover:scale-105">
                  Start Planning <ArrowRight className="ml-2 h-5 w-5" />
                </Button>
              </Link>
            </div>
          </div>
        </section>

        {/* Features Section */}
        <section id="features" className="py-24 bg-black border-t border-white/10">
          <div className="container mx-auto px-4 md:px-6">
            <div className="flex flex-col items-center justify-center text-center mb-16">
              <h2 className="text-3xl md:text-5xl font-bold tracking-tight mb-4">Smart planning, zero hassle.</h2>
              <p className="text-white/60 max-w-[600px] text-lg">We handle the logistics so you can focus on the experience. Every detail is meticulously planned and optimized.</p>
            </div>
            
            <div className="grid gap-8 md:grid-cols-3 max-w-5xl mx-auto">
              <div className="flex flex-col items-start p-8 rounded-3xl bg-white/5 border border-white/10 backdrop-blur-sm hover:bg-white/10 transition-colors">
                <div className="p-3 rounded-2xl bg-blue-500/20 text-blue-400 mb-6">
                  <Map className="h-8 w-8" />
                </div>
                <h3 className="text-xl font-bold mb-3">Hyper-Local Context</h3>
                <p className="text-white/60 leading-relaxed">Our AI integrates directly with Google Maps to ensure travel times, restaurant hours, and attraction locations make perfect sense.</p>
              </div>
              
              <div className="flex flex-col items-start p-8 rounded-3xl bg-white/5 border border-white/10 backdrop-blur-sm hover:bg-white/10 transition-colors">
                <div className="p-3 rounded-2xl bg-amber-500/20 text-amber-400 mb-6">
                  <Sun className="h-8 w-8" />
                </div>
                <h3 className="text-xl font-bold mb-3">Weather-Aware</h3>
                <p className="text-white/60 leading-relaxed">We check historical and forecasted weather patterns to ensure your outdoor hike isn't scheduled during a thunderstorm.</p>
              </div>
              
              <div className="flex flex-col items-start p-8 rounded-3xl bg-white/5 border border-white/10 backdrop-blur-sm hover:bg-white/10 transition-colors">
                <div className="p-3 rounded-2xl bg-emerald-500/20 text-emerald-400 mb-6">
                  <Tent className="h-8 w-8" />
                </div>
                <h3 className="text-xl font-bold mb-3">Tailored to You</h3>
                <p className="text-white/60 leading-relaxed">Whether you want to sleep under the stars or in a 5-star resort, the itinerary adapts perfectly to your budget and travel style.</p>
              </div>
            </div>
          </div>
        </section>
      </main>

      {/* Footer */}
      <footer className="border-t border-white/10 bg-black py-12">
        <div className="container mx-auto px-4 md:px-6 flex flex-col md:flex-row justify-between items-center gap-6">
          <div className="flex items-center gap-2">
            <Compass className="h-6 w-6 text-white/50" />
            <span className="text-xl font-bold tracking-tighter text-white/50">WanderAI</span>
          </div>
          <p className="text-sm text-white/40">© 2026 WanderAI. All rights reserved.</p>
        </div>
      </footer>
    </div>
  );
}
