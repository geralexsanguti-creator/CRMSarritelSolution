import { useState, useEffect, useRef } from "react";
import { motion, AnimatePresence } from "framer-motion";
import { MessageSquare, Send, X, Users, Bot, User, Minimize2, Paperclip } from "lucide-react";
import { Button } from "@/components/ui/button";
import { Input } from "@/components/ui/input";
import { ScrollArea } from "@/components/ui/scroll-area";
import { Tabs, TabsContent, TabsList, TabsTrigger } from "@/components/ui/tabs";
import * as signalR from "@microsoft/signalr";
import { cn } from "@/lib/utils";
import { useAuth } from "@/contexts/AuthContext";

interface Message {
  id: string;
  user: string;
  text: string;
  time: string;
  isMe: boolean;
  type: "p2p" | "group" | "ai";
}

export function SarriChat() {
  const { user } = useAuth();
  const [isOpen, setIsOpen] = useState(false);
  const [activeTab, setActiveTab] = useState("p2p");
  const [message, setMessage] = useState("");
  const [messages, setMessages] = useState<Message[]>([]);
  const [connection, setConnection] = useState<signalR.HubConnection | null>(null);
  const scrollRef = useRef<HTMLDivElement>(null);

  useEffect(() => {
    try {
      const newConnection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:5066/chatHub")
        .withAutomaticReconnect()
        .build();

      setConnection(newConnection);
    } catch (err) {
      console.error("SignalR Builder Error:", err);
    }
  }, []);

  useEffect(() => {
    if (connection && user) {
      connection.start()
        .then(() => {
          console.log("Connected to SignalR!");
          connection.on("ReceiveMessage", (sender, text, type) => {
            setMessages(prev => [...prev, {
              id: Math.random().toString(),
              user: sender,
              text,
              time: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
              isMe: sender === user?.nombre,
              type: type as any
            }]);
          });
        })
        .catch(e => {
          console.warn("SignalR connection attempt failed (Server might be starting):", e.message);
        });
        
      return () => {
        connection.stop().catch(err => console.error("SignalR Stop Error:", err));
      };
    }
  }, [connection, user]);

  useEffect(() => {
    if (scrollRef.current) {
        scrollRef.current.scrollTop = scrollRef.current.scrollHeight;
    }
  }, [messages, isOpen]);

  const handleSend = async () => {
    if (!message.trim() || !connection) return;

    try {
      await connection.invoke("SendMessage", user?.nombre || "Anónimo", message, activeTab);
      
      if (activeTab === "ai") {
          // Simulate AI response
          setTimeout(() => {
            setMessages(prev => [...prev, {
                id: Math.random().toString(),
                user: "SarriAI",
                text: "Estoy procesando tu solicitud... En breve te daré una respuesta contextual basada en tus datos.",
                time: new Date().toLocaleTimeString([], { hour: '2-digit', minute: '2-digit' }),
                isMe: false,
                type: "ai"
            }]);
          }, 1000);
      }
      
      setMessage("");
    } catch (e) {
      console.error(e);
    }
  };

  return (
    <div className="fixed bottom-6 right-20 z-50">
      <AnimatePresence>
        {isOpen && (
          <motion.div 
            initial={{ opacity: 0, scale: 0.8, y: 20 }}
            animate={{ opacity: 1, scale: 1, y: 0 }}
            exit={{ opacity: 0, scale: 0.8, y: 20 }}
            className="mb-4 w-80 md:w-96 glass-dark border border-white/10 rounded-2xl shadow-2xl overflow-hidden flex flex-col h-[500px]"
            style={{ backdropFilter: "blur(20px)" }}
          >
            <div className="p-4 bg-primary/20 flex items-center justify-between border-b border-white/10">
              <div className="flex items-center gap-2">
                <div className="h-8 w-8 rounded-full bg-primary flex items-center justify-center">
                  <Bot className="h-5 w-5 text-primary-foreground" />
                </div>
                <div>
                  <h3 className="text-sm font-bold text-white">SarriChat</h3>
                  <div className="flex items-center gap-1">
                    <div className="h-1.5 w-1.5 rounded-full bg-success animate-pulse" />
                    <span className="text-[10px] text-white/60">En línea</span>
                  </div>
                </div>
              </div>
              <Button variant="ghost" size="icon" onClick={() => setIsOpen(false)} className="text-white/60 hover:text-white hover:bg-white/10 h-8 w-8">
                <Minimize2 className="h-4 w-4" />
              </Button>
            </div>

            <Tabs defaultValue="p2p" className="flex-1 flex flex-col" onValueChange={setActiveTab}>
              <TabsList className="bg-transparent border-b border-white/5 rounded-none h-10 p-0">
                <TabsTrigger value="p2p" className="flex-1 data-[state=active]:bg-white/5 rounded-none border-b-2 border-transparent data-[state=active]:border-primary transition-all text-[10px] uppercase tracking-wider text-white/50 data-[state=active]:text-white">
                  <User className="h-3 w-3 mr-1" /> Privado
                </TabsTrigger>
                <TabsTrigger value="group" className="flex-1 data-[state=active]:bg-white/5 rounded-none border-b-2 border-transparent data-[state=active]:border-primary transition-all text-[10px] uppercase tracking-wider text-white/50 data-[state=active]:text-white">
                  <Users className="h-3 w-3 mr-1" /> Equipos
                </TabsTrigger>
                <TabsTrigger value="ai" className="flex-1 data-[state=active]:bg-white/5 rounded-none border-b-2 border-transparent data-[state=active]:border-primary transition-all text-[10px] uppercase tracking-wider text-white/50 data-[state=active]:text-white">
                  <Bot className="h-3 w-3 mr-1" /> SarriAI
                </TabsTrigger>
              </TabsList>

              <ScrollArea className="flex-1 p-4">
                <div className="space-y-4" ref={scrollRef}>
                  {messages.filter(m => m.type === activeTab).length === 0 && (
                    <div className="flex flex-col items-center justify-center h-40 opacity-30 text-center">
                      <MessageSquare className="h-10 w-10 mb-2" />
                      <p className="text-xs">No hay mensajes en {activeTab}</p>
                    </div>
                  )}
                  {messages.filter(m => m.type === activeTab).map((msg, i) => (
                    <div key={i} className={cn("flex flex-col", msg.isMe ? "items-end" : "items-start")}>
                      {!msg.isMe && <span className="text-[10px] text-white/40 mb-1 ml-2">{msg.user}</span>}
                      <div className={cn(
                        "max-w-[80%] p-3 rounded-2xl text-xs shadow-lg",
                        msg.isMe 
                          ? "bg-primary text-primary-foreground rounded-tr-none" 
                          : msg.user === "SarriAI" 
                            ? "bg-indigo-600/30 text-white border border-indigo-500/30 rounded-tl-none"
                            : "bg-white/10 text-white rounded-tl-none"
                      )}>
                        <p>{msg.text}</p>
                        <span className="text-[9px] opacity-40 mt-1 block text-right">{msg.time}</span>
                      </div>
                    </div>
                  ))}
                </div>
              </ScrollArea>

              <div className="p-4 border-t border-white/5 space-y-3">
                <div className="flex gap-2">
                  <Input 
                    value={message}
                    onChange={(e) => setMessage(e.target.value)}
                    onKeyDown={(e) => e.key === 'Enter' && handleSend()}
                    placeholder="Escribe un mensaje..."
                    className="flex-1 bg-white/5 border-white/10 text-white placeholder:text-white/30 h-10"
                  />
                  <Button size="icon" onClick={handleSend} disabled={!message.trim()} className="bg-primary hover:bg-primary/90 h-10 w-10 shrink-0 shadow-lg shadow-primary/20">
                    <Send className="h-4 w-4" />
                  </Button>
                </div>
                <div className="flex items-center justify-between px-1">
                  <button className="text-[10px] text-white/40 hover:text-white flex items-center gap-1 transition-colors">
                    <Paperclip className="h-3 w-3" /> Adjuntar Doc
                  </button>
                  {activeTab === "ai" && (
                    <span className="text-[9px] text-indigo-400 font-bold uppercase tracking-widest animate-pulse">
                      GPT-4o Contextual
                    </span>
                  )}
                </div>
              </div>
            </Tabs>
          </motion.div>
        )}
      </AnimatePresence>

      <motion.button
        whileHover={{ scale: 1.1, rotate: 5 }}
        whileTap={{ scale: 0.9 }}
        onClick={() => setIsOpen(!isOpen)}
        className={cn(
          "h-14 w-14 rounded-full flex items-center justify-center shadow-2xl relative transition-all duration-300",
          isOpen ? "bg-white/10 text-white rotate-90" : "bg-primary text-primary-foreground"
        )}
        style={{ boxShadow: isOpen ? "none" : "0 0 20px rgba(var(--primary-rgb), 0.4)" }}
      >
        {isOpen ? <X className="h-6 w-6" /> : <MessageSquare className="h-6 w-6" />}
        {!isOpen && (
          <span className="absolute -top-1 -right-1 h-5 w-5 bg-indigo-500 border-2 border-background rounded-full flex items-center justify-center text-[10px] font-bold">
            AI
          </span>
        )}
      </motion.button>
    </div>
  );
}
