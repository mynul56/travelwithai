"use client";

import { ReactNode } from "react";
import Link from "next/link";
import { LayoutDashboard, Ticket, Users, Settings } from "lucide-react";

export function AdminLayout({ children }: { children: ReactNode }) {
  return (
    <div className="min-h-screen flex bg-muted/20">
      {/* Sidebar */}
      <aside className="w-64 bg-background border-r flex flex-col">
        <div className="p-6">
          <h2 className="text-2xl font-bold text-primary tracking-tight">Admin Portal</h2>
        </div>
        <nav className="flex-1 px-4 space-y-2">
          <Link href="/admin" className="flex items-center gap-3 px-3 py-2 rounded-md hover:bg-muted text-foreground/80 hover:text-foreground transition-colors">
            <LayoutDashboard className="w-5 h-5" />
            Analytics
          </Link>
          <Link href="/admin/trips" className="flex items-center gap-3 px-3 py-2 rounded-md hover:bg-muted text-foreground/80 hover:text-foreground transition-colors">
            <Ticket className="w-5 h-5" />
            Trip Queue
          </Link>
          <Link href="/admin/users" className="flex items-center gap-3 px-3 py-2 rounded-md hover:bg-muted text-foreground/80 hover:text-foreground transition-colors">
            <Users className="w-5 h-5" />
            Users
          </Link>
        </nav>
        <div className="p-4 border-t">
          <Link href="/dashboard" className="flex items-center gap-3 px-3 py-2 rounded-md text-sm text-muted-foreground hover:text-foreground">
            <Settings className="w-4 h-4" />
            Back to App
          </Link>
        </div>
      </aside>

      {/* Main Content */}
      <main className="flex-1 overflow-y-auto">
        <header className="h-16 bg-background border-b flex items-center px-8 shadow-sm">
          <h1 className="text-xl font-semibold">Dashboard Overview</h1>
        </header>
        <div className="p-8">
          {children}
        </div>
      </main>
    </div>
  );
}
