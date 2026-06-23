"use client";

import { useState } from "react";
import { Table, TableBody, TableCell, TableHead, TableHeader, TableRow } from "@/components/ui/table";
import { Button } from "@/components/ui/button";
import { Badge } from "@/components/ui/badge";
import { MoreHorizontal, Eye, CheckCircle, XCircle, RefreshCw } from "lucide-react";
import { DropdownMenu, DropdownMenuContent, DropdownMenuItem, DropdownMenuTrigger } from "@/components/ui/dropdown-menu";

export function TripPackageQueue() {
  const [trips] = useState([
    { id: "1", destination: "Paris, France", user: "john@example.com", status: "PendingApproval", date: "2026-10-01" },
    { id: "2", destination: "Tokyo, Japan", user: "jane@example.com", status: "Generating", date: "2026-11-15" },
    { id: "3", destination: "Rome, Italy", user: "mark@example.com", status: "Approved", date: "2026-09-20" },
  ]);

  const handleAction = async (id: string, action: string) => {
    // API Call: POST /api/v1/admin/trips/{id}/{action}
    console.log(`Triggering ${action} for Trip ${id}`);
  };

  return (
    <div className="space-y-4 animate-in fade-in duration-500">
      <div className="flex justify-between items-center">
        <h2 className="text-2xl font-bold tracking-tight">Trip Requests Queue</h2>
      </div>

      <div className="rounded-md border bg-card">
        <Table>
          <TableHeader>
            <TableRow>
              <TableHead>Destination</TableHead>
              <TableHead>User</TableHead>
              <TableHead>Date</TableHead>
              <TableHead>Status</TableHead>
              <TableHead className="text-right">Actions</TableHead>
            </TableRow>
          </TableHeader>
          <TableBody>
            {trips.map((trip) => (
              <TableRow key={trip.id}>
                <TableCell className="font-medium">{trip.destination}</TableCell>
                <TableCell>{trip.user}</TableCell>
                <TableCell>{trip.date}</TableCell>
                <TableCell>
                  <Badge 
                    variant={trip.status === "Approved" ? "default" : trip.status === "PendingApproval" ? "secondary" : "outline"}
                    className={trip.status === "PendingApproval" ? "bg-amber-500/10 text-amber-500 hover:bg-amber-500/20" : ""}
                  >
                    {trip.status}
                  </Badge>
                </TableCell>
                <TableCell className="text-right">
                  <DropdownMenu>
                    <DropdownMenuTrigger>
                      <Button variant="ghost" className="h-8 w-8 p-0">
                        <MoreHorizontal className="h-4 w-4" />
                      </Button>
                    </DropdownMenuTrigger>
                    <DropdownMenuContent align="end">
                      <DropdownMenuItem className="gap-2">
                        <Eye className="w-4 h-4" /> View Details
                      </DropdownMenuItem>
                      {trip.status === "PendingApproval" && (
                        <>
                          <DropdownMenuItem className="gap-2 text-green-600" onClick={() => handleAction(trip.id, "approve")}>
                            <CheckCircle className="w-4 h-4" /> Approve
                          </DropdownMenuItem>
                          <DropdownMenuItem className="gap-2 text-red-600" onClick={() => handleAction(trip.id, "reject")}>
                            <XCircle className="w-4 h-4" /> Reject
                          </DropdownMenuItem>
                          <DropdownMenuItem className="gap-2 text-blue-600" onClick={() => handleAction(trip.id, "regenerate")}>
                            <RefreshCw className="w-4 h-4" /> Regenerate
                          </DropdownMenuItem>
                        </>
                      )}
                    </DropdownMenuContent>
                  </DropdownMenu>
                </TableCell>
              </TableRow>
            ))}
          </TableBody>
        </Table>
      </div>
    </div>
  );
}
